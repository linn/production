import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { boardTestsReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.boardTestsReport.actionType,
    actionTypes,
    defaultState
);