import { combineReducers } from 'redux';
import { reportResultsFactory } from '@linn-it/linn-form-components-library';
import { assemblyFailsWaitingListReportActionTypes as actionTypes } from '../actions';
import * as reportTypes from '../reportTypes';

const defaultState = { loading: false, data: null };

export default combineReducers(
    reportResultsFactory(
        reportTypes.assemblyFailsWaitingListReport.actionType,
        actionTypes,
        defaultState
    )
);
