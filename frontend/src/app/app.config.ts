import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';

import { HttpClient, HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { provideClientHydration, BrowserModule } from '@angular/platform-browser';
import { jwtFactory } from './jwt-options';
import { LocalService } from '../services/local.service';
import { JWT_OPTIONS, JwtModule } from '@auth0/angular-jwt';
import { errorInterceptor } from '../interceptor/errorHandingInterceptor';
import { routes } from './app.routes';
import {provideTranslateService, TranslateLoader} from "@ngx-translate/core";
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

const httpLoaderFactory: (http: HttpClient) => TranslateHttpLoader = (http: HttpClient) =>
  new TranslateHttpLoader(http, './i18n/', '.json');

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
  provideClientHydration(),
  provideTranslateService({
    loader: {
      provide: TranslateLoader,
      useFactory: httpLoaderFactory,
      deps: [HttpClient],
    },
  }),
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
