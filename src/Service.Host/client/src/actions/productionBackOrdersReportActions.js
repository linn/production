import { ReportActions } from '@linn-it/linn-form-components-library';
import { productionBackOrdersReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.productionBackOrdersReport.item,
    reportTypes.productionBackOrdersReport.actionType,
    reportTypes.productionBackOrdersReport.uri,
    actionTypes,
    config.appRoot
);
