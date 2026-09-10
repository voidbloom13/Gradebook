import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Alert } from './components/alert/alert';

@Component({
  imports: [RouterOutlet, Alert],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('Gradebook');
}
