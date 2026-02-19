import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { AuthService } from '../../services/auth'; // Ensure correct path

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class NavbarComponent implements OnInit {
  isLoggedIn = false; 

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    // Sync UI with the actual authentication state
    this.authService.isLoggedIn.subscribe(status => {
      this.isLoggedIn = status;
    });
  }

  onLogout() {
    // Logic to clear JWT and notify the AuthService
    localStorage.removeItem('token');
    this.authService.updateLoginStatus(false); 
    this.router.navigate(['/login']);
  }
}