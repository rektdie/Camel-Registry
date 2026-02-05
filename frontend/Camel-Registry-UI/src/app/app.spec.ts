import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app';
import { provideHttpClient } from '@angular/common/http';

describe('AppComponent form validation', () => {
  it('name must be at least 2 chars and humpCount must be 1 or 2', () => {
    TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [provideHttpClient()]
    });

    const fixture = TestBed.createComponent(AppComponent);
    const component = fixture.componentInstance;

    component.form.setValue({ name: 'A', humpCount: 3 });

    expect(component.form.valid).toBeFalsy();
    expect(component.form.get('name')!.valid).toBeFalsy();
    expect(component.form.get('humpCount')!.valid).toBeFalsy();

    component.form.setValue({ name: 'AA', humpCount: 2 });

    expect(component.form.valid).toBeTruthy();
  });
});
