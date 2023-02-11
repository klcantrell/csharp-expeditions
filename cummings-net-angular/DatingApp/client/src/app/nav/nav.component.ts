import { Component, OnInit } from '@angular/core';
import { firstValueFrom, Observable, of } from 'rxjs';
import { AccountService } from '../services/account.service';
import { LoginParams } from '../_models/loginParams';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: LoginParams = {
    username: "",
    password: "",
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
