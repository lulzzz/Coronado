import initialState from './initialState';
import { cloneDeep, concat, filter, orderBy, forEachRight, each } from 'lodash'; 
import * as actions from "../constants/accountActionTypes.js";

function computeRunningTotal(transactions) {
  var total = 0;
  var sorted = orderBy(transactions, ['transactionDate', 'enteredDate'], ['desc', 'desc']);
  forEachRight(sorted, (value) => {
    total += (value.credit - value.debit);
    value.runningTotal = total;
  });
  return sorted;
}

export const transactionReducer = (state = initialState.transactions, action) => {

  let transactions;
  switch (action.type) {
    
    case actions.LOAD_TRANSACTIONS_SUCCESS:
      transactions = computeRunningTotal(action.transactions);
      return transactions;

    case actions.DELETE_TRANSACTION:
      return computeRunningTotal(filter(state, (t) => {return t.transactionId !== action.transactionId}));

    case actions.CREATE_TRANSACTION_SUCCESS:
      transactions = cloneDeep(state);
      transactions = each(action.newTransaction, t => { concat(transactions, Object.assign({}, t))});
      return computeRunningTotal(transactions)

    case actions.UPDATE_TRANSACTION_SUCCESS:
      transactions = state.filter(t => t.transactionId !== action.updatedTransaction.transactionId);
      transactions = concat(transactions, Object.assign({}, action.updatedTransaction));
      return computeRunningTotal(transactions);

    default:
      return state;
  }
};
