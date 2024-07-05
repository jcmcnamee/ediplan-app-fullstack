import { Link } from './Link';

export interface Asset {
  id: number;
  name: string;
  type: string;
  links: Link[];
}
