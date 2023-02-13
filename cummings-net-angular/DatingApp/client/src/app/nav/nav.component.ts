import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AccountService } from '../services/account.service';
import { LoginParams } from '../_models/loginParams';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: LoginParams = {
    username: '',
    password: '',
  }

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
  }

  login() {
    firstValueFrom(this.accountService.login(this.model));
  }

  logout() {
    this.accountService.logout();
  }
}
