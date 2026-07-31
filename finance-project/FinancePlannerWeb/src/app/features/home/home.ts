import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [FormsModule, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

  private authService = inject(AuthService);
  private router = inject(Router);

  email = '';
  password = '';

  login() {

    const request = {
      email: this.email,
      password: this.password
    };

  this.authService.login(request)
  .subscribe({
    next: (response) => {

      localStorage.setItem(
        'token',
        response.token
      );

      this.router.navigate(['/dashboard']);

    },
    error: (err) => {

      console.error(err);

    }
  });

  }

}