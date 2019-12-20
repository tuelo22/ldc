import { CookieService } from 'ngx-cookie-service';
import { Component } from '@angular/core';
import { UUID } from 'angular2-uuid';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
})
export class HomePage {

  user: string

  constructor(private cookieService: CookieService) {

  }

  public ngOnInit(): void {
    this.user = 'fd3a411c-6b9b-4073-bbe3-b14193c9c927'//this.cookieService.get('user_id');

    if (this.user === null){
      this.user = this.generateUUID();
      this.cookieService.set('user_id', this.user);
    }
  }

  generateUUID(){
    return UUID.UUID();
  }

}
