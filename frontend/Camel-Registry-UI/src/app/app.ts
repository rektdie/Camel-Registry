import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CamelService } from './services/camel.service';
import { Camel } from './models/camel';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent implements OnInit {
  camels: Camel[] = [];
  errorMessage: string | null = null;

  constructor(private api: CamelService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.api.getAll().subscribe({
      next: data => {
        this.camels = data;
        this.cdr.detectChanges();
      },
      error: () => {
        this.errorMessage = 'Failed to load camels.';
        this.cdr.detectChanges();
      }
    });
  }

  edit(c: Camel): void { }
}
