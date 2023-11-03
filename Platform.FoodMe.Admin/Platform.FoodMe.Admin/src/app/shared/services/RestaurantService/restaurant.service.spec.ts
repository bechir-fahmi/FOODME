import { TestBed } from '@angular/core/testing';

import { RestourantService } from './restaurant.service';

describe('RestaurantService', () => {
  let service: RestourantService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RestourantService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
