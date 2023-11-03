export interface Restaurant {
  name: string;
  selected: boolean;
}

export interface Brand {
  name: string;
  restaurants: Restaurant[];
  selected: boolean;
}
