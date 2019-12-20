import { Lista } from './lista.model';
import { LDC_API } from './../../app.api';
import { Injectable, ErrorHandler } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from "rxjs/operators"; 

@Injectable()

export class ListaService {
    constructor(private http: HttpClient){}

    getlistas(user: string): Observable<Lista[]> {
        let par = new HttpParams().set('IdUsuario', user)
        
        return this.http.get<Lista[]>(`${LDC_API}/lista/Listar`, { params: par }).pipe(
            catchError(this.handleError)
          );      
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
          console.error('An error occurred:', error.error.message);
        } else {
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }

        return throwError('Alguma coisa ruim aconteceu. Por favor tente mais tarde.');
      };
}