export interface Camel {
  id: number;
  name: string;
  humpCount: number; // only 1 or 2
}

export type CamelCreateUpdate = Omit<Camel, 'id'>;
