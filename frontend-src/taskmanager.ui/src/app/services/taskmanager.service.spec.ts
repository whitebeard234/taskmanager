import { TestBed } from '@angular/core/testing';

import { TaskManagerService } from './taskmanager.service';

describe('TaskmanagerService', () => {
  let service: TaskManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
