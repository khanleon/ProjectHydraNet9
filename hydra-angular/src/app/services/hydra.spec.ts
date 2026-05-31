import { TestBed } from '@angular/core/testing';

import { Hydra } from './hydra';

describe('Hydra', () => {
  let service: Hydra;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Hydra);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
