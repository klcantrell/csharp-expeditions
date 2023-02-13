import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
  }

  async login() {
    await firstValueFrom(this.accountService.login(this.model));
    this.router.navigateByUrl('/members');
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
