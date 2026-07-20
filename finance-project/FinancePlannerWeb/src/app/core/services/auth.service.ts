import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  private api = `${environment.apiUrl}/auth`;

  register(request: any) {
    return this.http.post(`${this.api}/register`, request);
  }

  login(request: any) {
    return this.http.post(`${this.api}/login`, request);
  }

}