import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
  }

  async login() {
    this.accountService.login(this.model).subscribe({
      complete: () => {
        this.router.navigateByUrl('/members');
      },
      error: () => {
        this.toastr.error('Something went wrong logging you in. Please try again.');
      },
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
