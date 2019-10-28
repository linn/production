import { ReportActions } from '@linn-it/linn-form-components-library';
import { assemblyFailsMeasuresActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.assemblyFailsMeasures.item,
    reportTypes.assemblyFailsMeasures.actionType,
    reportTypes.assemblyFailsMeasures.uri,
    actionTypes,
    config.appRoot
);
