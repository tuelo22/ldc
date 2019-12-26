import { BaseService } from './../base.service';
import { Usuario } from './usuario.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LDC_API } from 'src/app/app.api';

@Injectable()

export class LoginService extends BaseService {
    constructor(private http: HttpClient) {
        super();
    }

    public createTempUser(): Observable<Usuario> {
        return this.http.get<Usuario>(`${LDC_API}/usuario/AddTemp`).pipe(
            catchError(this.handleError));

    }
}
