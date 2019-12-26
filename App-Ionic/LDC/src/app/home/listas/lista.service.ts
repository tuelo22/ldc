import { Lista } from './lista.model';
import { LDC_API } from './../../app.api';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { BaseService } from '../base.service';

@Injectable()

export class ListaService extends BaseService {
    constructor(private http: HttpClient){
      super();
    }

    getlistas(user: string): Observable<Lista[]> {
        const par = new HttpParams().set('IdUsuario', user);

        return this.http.get<Lista[]>(`${LDC_API}/lista/Listar`, { params: par }).pipe(
            catchError(this.handleError)
          );
    }
}
