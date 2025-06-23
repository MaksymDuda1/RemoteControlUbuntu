import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import {throwError } from 'rxjs';
import {catchError } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.log('Full error object:', error);

      let errorMessage = 'Unknown error!';

      if (error.error instanceof ErrorEvent) {
        // Client-side error
        console.error('Client error:', error.error.message);
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // Server-side error
        console.log('Server error:', error.error);

        // Handle different error response structures
        if (error.error && error.error.title) {
          // Your API returns: { status: 401, title: "Wrong data" }
          errorMessage = error.error.title;
        } else if (error.error && error.error.message) {
          // Alternative structure: { message: "Error message" }
          errorMessage = error.error.message;
        } else if (error.error && typeof error.error === 'string') {
          // Error is a string
          errorMessage = error.error;
        } else if (error.message) {
          // Fallback to HTTP error message
          errorMessage = error.message;
        } else {
          // Ultimate fallback
          errorMessage = `Server error: ${error.status} ${error.statusText}`;
        }
      }

      console.error('Final error message:', errorMessage);
      return throwError(() => errorMessage);
    })
  );
};


