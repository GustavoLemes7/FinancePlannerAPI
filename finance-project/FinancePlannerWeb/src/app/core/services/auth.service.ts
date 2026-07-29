import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { LoginResponse } from '../interfaces/auth/login-response';
import { LoginRequest } from '../interfaces/auth/login-request';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';



interface JwtPayload {
  exp: number;
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private http = inject(HttpClient);

  private api = `${environment.apiUrl}/auth`;

  private router = inject(Router);

  register(request: any) {
    return this.http.post(`${this.api}/register`, request);
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/auth/login`,
      request
    );
  }

  

  isLoggedIn(): boolean {

    const token = localStorage.getItem('token');

    if (!token)
      return false;

    try {

      const decoded = jwtDecode<JwtPayload>(token);

      const now = Math.floor(Date.now() / 1000);

      return decoded.exp > now;

    }
    catch {

      return false;

    }

  }

  logout(): void{
      localStorage.removeItem('token');

      this.router.navigate(['/']);
  }

}