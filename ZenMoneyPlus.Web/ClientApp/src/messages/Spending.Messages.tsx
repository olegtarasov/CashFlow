export type SpendingMode = "month" | "year";

export interface MonthAndYear {
    month: number;
    year: number;
}

export interface MonthRange {
    from: MonthAndYear;
    to: MonthAndYear;
}

export interface YearRange {
    from: number;
    to: number;
}

export interface SpendingRequest {
    mode: SpendingMode;
    monthRange: MonthRange;
    yearRange: YearRange;
    tags: string[];
}

export type SpendingRequestReduceAction =
    | { type: "mode", mode: SpendingMode }
    | { type: "monthRange", monthRange: MonthRange }
    | { type: "tags", tags: string[] };

export function spendingRequestReducer(request: SpendingRequest, action: SpendingRequestReduceAction) {
    switch (action.type) {
        case "tags":
            return {...request, tags: action.tags};
        case "mode":
            return {...request, mode: action.mode};
        case "monthRange":
            return {...request, monthRange: action.monthRange};
        default:
            throw new Error("Unsupported request reducer action");
    }
}

export const INITIAL_SPENDING_REQUEST: SpendingRequest = {
    mode: "month",
    monthRange: {from: {year: 2019, month: 1}, to: {year: 2022, month: 12}},
    yearRange: {from: 2017, to: 2022},
    tags: []
};