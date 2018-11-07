import * as types from '../constants/transactionActionTypes';
import * as selectedAccountTypes from "../constants/selectAccountActionTypes";
import TransactionApi from '../api/transactionApi';

export function loadTransactionsSuccess(transactions, accountId) {
  return { type: types.LOAD_TRANSACTIONS_SUCCESS, transactions, accountId };
}

export function selectAccount(accountId) {
  return { type: selectedAccountTypes.SELECT_ACCOUNT, accountId };
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
    dispatch(selectAccount(accountId) );
    const transactions = await TransactionApi.getTransactions(accountId);

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

    const updatedTransactionModel = await TransactionApi.updateTransaction(transaction);
    dispatch(updateTransactionSuccess(updatedTransactionModel));
  }
}

export const createTransaction = (transaction, transactionType) => {
  return async (dispatch) => {

    const newTransaction = await TransactionApi.createTransaction(transaction, transactionType);

    dispatch(createTransactionSuccess(newTransaction));
  }
}