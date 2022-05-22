import {MonthRange} from "../components/MonthPicker";

export type SpendingMode = "month" | "year";

export interface SpendingRequest {
    mode: SpendingMode;
    monthRange: MonthRange;
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
    tags: []
};