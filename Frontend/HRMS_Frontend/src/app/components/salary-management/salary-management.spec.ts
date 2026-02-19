import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SalaryManagementComponent } from './salary-management'; // FIX: Correct class reference

describe('SalaryManagementComponent', () => {
  let component: SalaryManagementComponent;
  let fixture: ComponentFixture<SalaryManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SalaryManagementComponent] // FIX: Use standalone component class
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalaryManagementComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});