import { Usuario } from './login/usuario.model';
import { LoginService } from './login/login.service';
import { CookieService } from 'ngx-cookie-service';
import { Component } from '@angular/core';
import { UUID } from 'angular2-uuid';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
})
export class HomePage {

  user: Usuario;
  idUsuario: string;

  constructor(private cookieService: CookieService, private loginService: LoginService) {}

  public ngOnInit(): void {
    this.idUsuario = this.cookieService.get('user_id');

    if (this.idUsuario === null) {
      this.loginService.createTempUser().subscribe(x => this.user = x);
      this.idUsuario = this.user.id;
      this.cookieService.set('user_id', this.idUsuario);
    }
  }
}
