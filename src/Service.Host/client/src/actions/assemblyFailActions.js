import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { assemblyFailActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.assemblyFail.item,
    itemTypes.assemblyFail.actionType,
    itemTypes.assemblyFail.uri,
    actionTypes,
    config.appRoot
);
