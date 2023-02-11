import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating app';
  users: User[] = [];

  constructor(private http: HttpClient, private accountService: AccountService) {}

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers() {
    this.http.get<User[]>('https://localhost:5001/api/users').subscribe({
      next: (response: User[]) => this.users = response,
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }

  setCurrentUser() {
    const persistedUser = localStorage.getItem('user');
    if (persistedUser != null) {
      const user = JSON.parse(persistedUser) as User;
      this.accountService.setCurrentUser(user);
    }
  }
}
