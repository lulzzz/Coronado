import * as types from '../constants/investmentActionTypes';
import * as transactionTypes from '../constants/transactionActionTypes';
import InvestmentApi from '../api/investmentApi';
import { info } from 'react-notification-system-redux';
import { authHeader } from '../api/auth-header';

export function loadInvestmentsSuccess(investments) {
  return {type: types.LOAD_INVESTMENTS_SUCCESS, investments};
}

export function loadInvestmentsAction() {
  return {type: types.LOAD_INVESTMENTS};
}

export function createInvestmentSuccess(investment) {
  return {type: types.CREATE_INVESTMENT_SUCCESS, investment};
}

export function updateInvestmentSuccess(investment) {
  return {type: types.UPDATE_INVESTMENT_SUCCESS, investment};
}

export function makeCorrectingEntriesSuccess(correctingEntryModel) {
  return {type: transactionTypes.CREATE_TRANSACTION_SUCCESS, ...correctingEntryModel};
}

export const loadInvestments = () => {
  return async (dispatch) => {
    dispatch(loadInvestmentsAction());
    const investments = await InvestmentApi.getAllInvestments();
    dispatch(loadInvestmentsSuccess(investments));
  };
}

export const makeCorrectingEntries = () => {
  return async (dispatch) => {
    const correctingEntryModel = await InvestmentApi.makeCorrectingEntries();
    if (correctingEntryModel && correctingEntryModel !== "")
    {

      const notificationOpts = {
        message: 'The entry to sync the investments with the investment account has been created',
        position: 'br',
        level: 'success',
        autoDismiss: 5,
        dismissible: 'click',
        title: 'Adjusting entry created'
      };
      dispatch(info(notificationOpts));
      dispatch(makeCorrectingEntriesSuccess(correctingEntryModel));
    } else {
      const notificationOpts = {
        message: 'Either no entry is necessary or no investment account was found.',
        position: 'br',
        level: 'warning',
        autoDismiss: 5,
        dismissible: 'click',
        title: 'No entry created'
      };
      dispatch(info(notificationOpts));
    }
  }
}

export const updateInvestment = (investment) => {
  return async (dispatch) => {
    const updatedInvestment = await InvestmentApi.updateInvestment(investment);
    dispatch(updateInvestmentSuccess(updatedInvestment));
  }
}

export const createInvestment = (investment) => {
  return async (dispatch) => {
    const newInvestment = await InvestmentApi.createInvestment(investment);
    dispatch(createInvestmentSuccess(newInvestment));
  }
}

export const deleteInvestment = (investmentId, investmentName) => {
  return async function(dispatch, getState) {
    const notificationOpts = {
      message: 'Investment ' + investmentName + ' deleted',
      position: 'br',
      onRemove: () => { deleteInvestmentForReal(investmentId, getState().deletedInvestments) },
      action: {
        label: 'Undo',
        callback: () => {dispatch({type: types.UNDO_DELETE_INVESTMENT, investmentId })}
      }
    };
    dispatch( { type: types.DELETE_INVESTMENT, investmentId } );
    dispatch(info(notificationOpts));
  }
}

async function deleteInvestmentForReal(investmentId, deletedInvestments) {
  if (deletedInvestments.some(i => i.investmentId === investmentId)) {
    await fetch('/api/Investments/' + investmentId, {
      method: 'DELETE',
      headers: {
        ...authHeader(),
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      }
    });
  }
}
