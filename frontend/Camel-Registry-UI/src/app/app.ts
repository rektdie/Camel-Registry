import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CamelService } from './services/camel.service';
import { Camel } from './models/camel';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent implements OnInit {
  camels: Camel[] = [];
  errorMessage: string | null = null;

  editingId: number | null = null;
  form;

  constructor(private api: CamelService, private fb: FormBuilder) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      humpCount: [null as number | null, [Validators.required, Validators.min(1), Validators.max(2)]]
    });
  }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.errorMessage = null;

    this.api.getAll().subscribe({
      next: (data) => (this.camels = data),
      error: () => (this.errorMessage = 'Failed to load camels.')
    });
  }

  edit(camel: Camel): void {
    this.editingId = camel.id;
    this.form.setValue({ name: camel.name, humpCount: camel.humpCount });
  }

  clear(): void {
    this.editingId = null;
    this.form.reset({ name: '', humpCount: null });
  }

  save(): void {
    this.errorMessage = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload = {
      name: this.form.value.name!,
      humpCount: this.form.value.humpCount!
    };

    const request =
      this.editingId === null
        ? this.api.create(payload)
        : this.api.update(this.editingId, payload);

    request.subscribe({
      next: () => {
        this.clear();
        this.load();
      },
      error: () => (this.errorMessage = 'Failed to save camel.')
    });
  }

  isInvalid(field: 'name' | 'humpCount'): boolean {
    const control = this.form.get(field);
    return !!control && control.invalid && control.touched;
  }
}
