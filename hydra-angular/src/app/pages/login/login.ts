import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Hydra } from '../../services/hydra';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  username = signal('admin');
  password = signal('password');

  constructor(public hydra: Hydra) {}

  login() {
    this.hydra.login(
      this.username(),
      this.password()
    );
  }
}
