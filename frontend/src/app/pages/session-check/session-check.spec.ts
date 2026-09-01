import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SessionCheck } from './session-check';

describe('SessionCheck', () => {
  let component: SessionCheck;
  let fixture: ComponentFixture<SessionCheck>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SessionCheck],
    }).compileComponents();

    fixture = TestBed.createComponent(SessionCheck);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
