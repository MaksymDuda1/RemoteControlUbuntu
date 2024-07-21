import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import {throwError } from 'rxjs';
import {catchError } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            let errorMessage = 'Unknown error!';
            if (error.error instanceof ErrorEvent) {
                errorMessage = `Error: ${error.error.message}`;
            } else {
                try {
                    const errorBody = JSON.parse(error.error);
                    errorMessage = errorBody.title || 'Unknown server error';
                } catch (e) {
                    errorMessage = 'Could not parse error response';
                }
            }
            return throwError(errorMessage);
        })
    );
};

