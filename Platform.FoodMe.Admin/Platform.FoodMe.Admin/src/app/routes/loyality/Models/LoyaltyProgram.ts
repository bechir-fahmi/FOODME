
export interface LoyaltyProgram {
  id: number;
  name: string;
  description: string;
  cashBackMode: number;
  status: number;
  conditions: Condition[];
  assignee: Assignee[];
  loyaltyPointRules: LoyaltyPointRule[];
  rating: Rating;
  unitPointEquivalent: number;
}

export interface Condition {
  id: number;
  conditionType: number;
  loyaltyProgramId: number;
}

export interface Assignee {
  assignementId: number;
  assignementType: number;
  assigneeId: number;
  loyaltyProgramId: number;
}

export interface LoyaltyPointRule {
  id: number;
  pointsRuleType: number;
  fixedPoints: number;
  pourcentagePoints: number;
}

export interface Dimension {
  id: number;
  ratingType: number;
}

export interface Rating {
  id: number;
  dimensions: Dimension[];
}
