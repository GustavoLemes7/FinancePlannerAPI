export interface Investment {

    userId: number;

    name: string;

    type: string;

    initialRate: number;

    startRate: number;

    contributions: unknown[];

    id: number;

    publicId: string;

    createdAt: string;

    updatedAt: string | null;

}