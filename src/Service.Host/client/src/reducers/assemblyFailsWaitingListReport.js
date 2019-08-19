import { combineReducers } from 'redux';
import { reportResultsFactory } from '@linn-it/linn-form-components-library';
import { assemblyFailsWaitingListReportActionTypes as actionTypes } from '../actions';
import * as reportTypes from '../reportTypes';

const defaultState = { loading: false, data: null };

const results = reportResultsFactory(
    reportTypes.assemblyFailsWaitingList.actionType,
    actionTypes,
    defaultState
);

export default combineReducers({ results });
