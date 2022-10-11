import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetActivatioUserComponent } from './set-activatio-user.component';

describe('SetActivatioUserComponent', () => {
  let component: SetActivatioUserComponent;
  let fixture: ComponentFixture<SetActivatioUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetActivatioUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetActivatioUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
