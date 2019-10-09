import { ReportActions } from '@linn-it/linn-form-components-library';
import { productionTriggerFactsTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.productionTriggerFacts.item,
    reportTypes.productionTriggerFacts.actionType,
    reportTypes.productionTriggerFacts.uri,
    actionTypes,
    config.appRoot
);
