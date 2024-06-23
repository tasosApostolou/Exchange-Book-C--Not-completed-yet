import { Component } from '@angular/core';


import { RouterLink, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar-component/navbar.component';

import { WelcomeComponent } from './components/welcome/welcome.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterLink, RouterOutlet,NavbarComponent,WelcomeComponent
    // BrowserAnimationsModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {}
