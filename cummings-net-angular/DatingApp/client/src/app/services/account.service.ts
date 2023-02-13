import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

import { LoginParams } from '../_models/loginParams';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: LoginParams) {
    return this.http.post<User>(`${this.baseUrl}/account/login`, model).pipe(
      map((user) => {
        localStorage.setItem('user', JSON.stringify(user));
        this.setCurrentUser(user);
        return user;
      })
    );
  }

  register(model: LoginParams) {
    return this.http.post<User>(`${this.baseUrl}/account/register`, model).pipe(
      map((user) => {
        localStorage.setItem('user', JSON.stringify(user));
        this.setCurrentUser(user);
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.setCurrentUser(null);
  }

  setCurrentUser(user: User | null) {
    this.currentUserSource.next(user);
  }
}
