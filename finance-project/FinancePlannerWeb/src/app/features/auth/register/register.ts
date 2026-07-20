import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  private authService = inject(AuthService);

  name = '';
  email = '';
  password = '';

  register() {

    const request = {
      name: this.name,
      email: this.email,
      password: this.password
    };

    this.authService.register(request)
      .subscribe({
        next: () => {

          console.log('Usuário criado.');

        },
        error: (err) => {

          console.error(err);

        }
      });

  }

}