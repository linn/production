import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { daysRequiredReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { citCode: 'S' };

export default reportOptionsFactory(
    reportTypes.daysRequiredReport.actionType,
    actionTypes,
    defaultState
);
