import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { productionTriggersReportTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = { citCode: '', jobref: '', reportType: 'Brief' };

export default reportOptionsFactory(
    reportTypes.productionTriggersReport.actionType,
    actionTypes,
    defaultState
);
