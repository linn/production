﻿import { ReportActions } from '@linn-it/linn-form-components-library';
import { smtOutstandingWorkOrderPartsActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.smtOutstandingWorkOrderParts.actionType,
    reportTypes.smtOutstandingWorkOrderParts.uri,
    actionTypes,
    config.appRoot
);
