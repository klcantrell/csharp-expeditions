import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

import { LoginParams } from '../_models/loginParams';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter<boolean>();

  model: LoginParams = {
    username: '',
    password: '',
  };

  constructor(
    private accountService: AccountService,
    private toastrService: ToastrService,
  ) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe({
      complete: () => {
        this.cancel();
      },
      error: () => {
        this.toastrService.error('Something went wrong registering you. Please try again.');
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
