import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [FormsModule, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

  private authService = inject(AuthService);

  email = '';
  password = '';

  login() {

    const request = {
      email: this.email,
      password: this.password
    };

    this.authService.login(request)
      .subscribe({
        next: () => {

          console.log('Usuário logado.');

        },
        error: (err) => {

          console.error(err);

        }
      });

  }

}