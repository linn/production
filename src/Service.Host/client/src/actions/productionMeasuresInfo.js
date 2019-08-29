import { ReportActions } from '@linn-it/linn-form-components-library';
import { productionMeasuresInfoActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.productionMeasuresInfoReport.actionType,
    reportTypes.productionMeasuresInfoReport.uri,
    actionTypes,
    config.appRoot
);
