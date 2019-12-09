import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { buildPlansReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.buildPlansReport.actionType,
    actionTypes,
    defaultState
);
