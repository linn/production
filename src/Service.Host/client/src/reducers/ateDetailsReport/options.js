import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { ateDetailsReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.ateDetailsReport.actionType,
    actionTypes,
    defaultState
);
