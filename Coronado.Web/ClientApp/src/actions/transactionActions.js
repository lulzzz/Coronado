import * as types from '../constants/transactionActionTypes';
import * as selectedAccountTypes from "../constants/selectAccountActionTypes";
import TransactionApi from '../api/transactionApi';
import { authHeader } from '../api/auth-header';

export function loadTransactionsSuccess(model, accountId) {
  return { 
    type: types.LOAD_TRANSACTIONS_SUCCESS, 
    transactions: model.transactions, 
    startingBalance: model.startingBalance,
    page: model.page,
    remainingTransactionCount: model.remainingTransactionCount,
    accountId };
}

export function selectAccount(accountId) {
  return { type: selectedAccountTypes.SELECT_ACCOUNT, accountId };
}

export function updateTransactionSuccess(updatedTransactionModel) {
  return { type: types.UPDATE_TRANSACTION_SUCCESS, ...updatedTransactionModel };
}

export function createTransactionSuccess(newTransactionModel) {
  return { type: types.CREATE_TRANSACTION_SUCCESS, ...newTransactionModel };
}

export function deleteTransactionSuccess(deleteTransactionModel) {
  return { type: types.DELETE_TRANSACTION_SUCCESS, ...deleteTransactionModel };
}

export const loadTransactions = (accountId) => {
  return async (dispatch) => {
    dispatch(selectAccount(accountId) );
    const transactionModel = await TransactionApi.getTransactions(accountId);

    dispatch(loadTransactionsSuccess(transactionModel, accountId));
  }
}

export const loadAllTransactions = (accountId) => {
  return async (dispatch) => {
    dispatch(selectAccount(accountId) );
    const transactions = await TransactionApi.getAllTransactions(accountId);

    dispatch(loadTransactionsSuccess(transactions, accountId));
  }
}

export const deleteTransaction = (transactionId) => {
  return async (dispatch) => {
    const response = await fetch('/api/Transactions/' + transactionId, {
      method: 'DELETE',
      headers: {
        ...authHeader(),
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