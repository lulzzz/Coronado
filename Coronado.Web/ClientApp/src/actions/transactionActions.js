import * as types from '../constants/transactionActionTypes';
import * as accountTypes from '../constants/accountActionTypes';
import AccountApi from '../api/accountApi';

export function loadTransactionsSuccess(transactions, accountId) {
  return { type: types.LOAD_TRANSACTIONS_SUCCESS, transactions, accountId };
}

export function selectAccount(accountId) {
  return { type: accountTypes.SELECT_ACCOUNT, accountId };
}

export function requestTransactions() {
  return { type: types.LOAD_TRANSACTIONS };
}

export function updateTransactionSuccess(updatedTransactionModel) {
  return { type: types.UPDATE_TRANSACTION_SUCCESS, ...updatedTransactionModel };
}

export function createTransactionSuccess(newTransaction) {
  return { type: types.CREATE_TRANSACTION_SUCCESS, newTransaction };
}

export function deleteTransactionSuccess(transaction) {
  return { type: types.DELETE_TRANSACTION_SUCCESS, transaction };
}

export const loadTransactions = (accountId) => {
  return async (dispatch) => {
    dispatch(requestTransactions());
    dispatch(selectAccount(accountId) );
    const transactions = await AccountApi.getTransactions(accountId);

    dispatch(loadTransactionsSuccess(transactions, accountId));
  }
}

export const deleteTransaction = (transactionId) => {
  return async (dispatch) => {
    const response = await fetch('/api/Transactions/' + transactionId, {
      method: 'DELETE',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      }
    });
    const transaction = await response.json();
    dispatch( deleteTransactionSuccess(transaction) );
  }
}

export const updateTransaction = (transaction) => {
  return async (dispatch) => {

    const updatedTransactionModel = await AccountApi.updateTransaction(transaction);
    dispatch(updateTransactionSuccess(updatedTransactionModel));
  }
}

export const createTransaction = (transaction, transactionType) => {
  return async (dispatch) => {

    const newTransaction = await AccountApi.createTransaction(transaction, transactionType);

    dispatch(createTransactionSuccess(newTransaction));
  }
}