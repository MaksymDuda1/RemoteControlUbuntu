import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';

import { routes } from './app.routes';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { provideClientHydration, BrowserModule } from '@angular/platform-browser';
import { jwtFactory } from './jwt-options';
import { LocalService } from '../services/local.service';
import { JWT_OPTIONS, JwtModule } from '@auth0/angular-jwt';
import { errorInterceptor } from '../interceptor/errorHandingInterceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
  provideClientHydration(),
  provideHttpClient(
    withInterceptors([errorInterceptor]),),
  importProvidersFrom([
    FormsModule,
    RouterModule,
    HttpClientModule,
    BrowserModule,
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtFactory,
        deps: [LocalService]
      }
    }),
  ])]
};
